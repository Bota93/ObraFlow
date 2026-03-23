import { httpClient } from '../../../shared/api/http-client'
import type { DailyReportListItem } from '../types/daily-reports.types'

function isDailyReportListItem(value: unknown): value is DailyReportListItem {
  if (typeof value !== 'object' || value === null) {
    return false
  }

  const record = value as Record<string, unknown>

  return (
    typeof record.id === 'string' &&
    typeof record.date === 'string' &&
    typeof record.workerId === 'string' &&
    typeof record.workerName === 'string' &&
    typeof record.hoursWorked === 'number' &&
    Number.isFinite(record.hoursWorked) &&
    typeof record.description === 'string'
  )
}

export async function getDailyReports() {
  const response = await httpClient.get<DailyReportListItem[]>('/daily-reports')

  if (!Array.isArray(response.data) || !response.data.every(isDailyReportListItem)) {
    throw new Error('Daily reports response is invalid.')
  }

  return response.data
}
