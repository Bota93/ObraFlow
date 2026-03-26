function normalizeBaseUrl(value: string) {
  return value.replace(/\/+$/, '')
}

const configuredApiBaseUrl = import.meta.env.VITE_API_BASE_URL?.trim()
const developmentFallbackBaseUrls = [
  'http://localhost:5250',
  'http://localhost:5000',
]
const apiBaseUrl =
  configuredApiBaseUrl ??
  (import.meta.env.DEV ? developmentFallbackBaseUrls[0] : undefined)

if (!apiBaseUrl) {
  throw new Error('VITE_API_BASE_URL must be defined for production builds.')
}

export const env = {
  apiBaseUrl: normalizeBaseUrl(apiBaseUrl),
  apiFallbackBaseUrls:
    import.meta.env.DEV && !configuredApiBaseUrl
      ? developmentFallbackBaseUrls
          .map(normalizeBaseUrl)
          .filter((value) => value !== normalizeBaseUrl(apiBaseUrl))
      : [],
  hasConfiguredApiBaseUrl: Boolean(configuredApiBaseUrl),
}
