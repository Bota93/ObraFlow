import { httpClient } from '../../../shared/api/http-client'
import type { DashboardSummary } from '../types/dashboard.types'

export async function getDashboardSummary() {
  const response = await httpClient.get<DashboardSummary>('/dashboard/summary')
  return response.data
}
