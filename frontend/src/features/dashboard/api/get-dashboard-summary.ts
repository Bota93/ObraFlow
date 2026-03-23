import { httpClient } from '../../../shared/api/http-client'
import type { DashboardSummary } from '../types/dashboard.types'

const dashboardNumericFields: Array<keyof DashboardSummary> = [
  'totalWorkers',
  'activeWorkers',
  'totalDailyReports',
  'openIncidents',
  'inProgressIncidents',
  'resolvedIncidents',
  'hoursWorkedToday',
  'hoursWorkedLast7Days',
]

function isDashboardSummary(value: unknown): value is DashboardSummary {
  if (typeof value !== 'object' || value === null) {
    return false
  }

  const record = value as Record<string, unknown>

  return dashboardNumericFields.every((field) => {
    const fieldValue = record[field]
    return typeof fieldValue === 'number' && Number.isFinite(fieldValue)
  })
}

export async function getDashboardSummary() {
  const response = await httpClient.get<DashboardSummary>('/dashboard/summary')

  if (!isDashboardSummary(response.data)) {
    throw new Error('Dashboard summary response is invalid.')
  }

  return response.data
}
