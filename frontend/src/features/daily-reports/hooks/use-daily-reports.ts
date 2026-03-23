import { useQuery } from '@tanstack/react-query'
import { getDailyReports } from '../api/get-daily-reports'

export function useDailyReports() {
  return useQuery({
    queryKey: ['daily-reports', 'list'],
    queryFn: getDailyReports,
  })
}
