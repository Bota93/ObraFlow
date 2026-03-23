import { httpClient } from '../../../shared/api/http-client'
import type { DailyReportListItem } from '../types/daily-reports.types'

export async function getDailyReports() {
  const response = await httpClient.get<DailyReportListItem[]>('/daily-reports')
  return response.data
}
