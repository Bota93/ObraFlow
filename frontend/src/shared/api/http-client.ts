import axios, { isAxiosError, type InternalAxiosRequestConfig } from 'axios'
import { env } from '../config/env'

type RetriableRequestConfig = InternalAxiosRequestConfig & {
  _obraflowRetriedWithFallback?: boolean
}

let resolvedApiBaseUrl = env.apiBaseUrl

export const httpClient = axios.create({
  baseURL: resolvedApiBaseUrl,
  headers: {
    'Content-Type': 'application/json',
  },
})

httpClient.interceptors.request.use((config) => {
  if (!env.hasConfiguredApiBaseUrl) {
    config.baseURL = resolvedApiBaseUrl
  }

  return config
})

httpClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (
      !isAxiosError(error) ||
      env.hasConfiguredApiBaseUrl ||
      env.apiFallbackBaseUrls.length === 0 ||
      error.response
    ) {
      return Promise.reject(error)
    }

    const requestConfig = error.config as RetriableRequestConfig | undefined
    if (!requestConfig || requestConfig._obraflowRetriedWithFallback) {
      return Promise.reject(error)
    }

    const fallbackBaseUrl = env.apiFallbackBaseUrls.find(
      (value) => value !== (requestConfig.baseURL ?? resolvedApiBaseUrl),
    )

    if (!fallbackBaseUrl) {
      return Promise.reject(error)
    }

    requestConfig._obraflowRetriedWithFallback = true
    requestConfig.baseURL = fallbackBaseUrl
    resolvedApiBaseUrl = fallbackBaseUrl

    return httpClient.request(requestConfig)
  },
)
