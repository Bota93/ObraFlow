import { useQuery } from '@tanstack/react-query'
import type { DailyReportListItem } from '../types/daily-reports.types'
import { getDailyReports } from '../api/get-daily-reports'

export function useDailyReports() {
  return useQuery<DailyReportListItem[], Error>({
    queryKey: ['daily-reports', 'list'],
    queryFn: getDailyReports,
  })
}
