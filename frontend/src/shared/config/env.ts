const fallbackApiBaseUrl = 'http://localhost:5250'

export const env = {
  apiBaseUrl: import.meta.env.VITE_API_BASE_URL ?? fallbackApiBaseUrl,
}
