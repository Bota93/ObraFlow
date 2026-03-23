import { useQuery } from '@tanstack/react-query'
import type { DashboardSummary } from '../types/dashboard.types'
import { getDashboardSummary } from '../api/get-dashboard-summary'

export function useDashboardSummary() {
  return useQuery<DashboardSummary, Error>({
    queryKey: ['dashboard', 'summary'],
    queryFn: getDashboardSummary,
  })
}
