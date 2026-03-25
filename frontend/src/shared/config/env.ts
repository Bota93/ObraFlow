const localApiBaseUrl = 'http://localhost:5250'
const configuredApiBaseUrl = import.meta.env.VITE_API_BASE_URL
const apiBaseUrl =
  configuredApiBaseUrl ?? (import.meta.env.DEV ? localApiBaseUrl : undefined)

if (!apiBaseUrl) {
  throw new Error('VITE_API_BASE_URL must be defined for production builds.')
}

export const env = {
  apiBaseUrl: apiBaseUrl.replace(/\/+$/, ''),
}
