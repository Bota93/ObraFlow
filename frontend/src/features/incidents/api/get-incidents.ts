import { httpClient } from '../../../shared/api/http-client'
import type { IncidentListItem } from '../types/incidents.types'

function isIncidentListItem(value: unknown): value is IncidentListItem {
  if (typeof value !== 'object' || value === null) {
    return false
  }

  const record = value as Record<string, unknown>

  return (
    typeof record.id === 'string' &&
    typeof record.title === 'string' &&
    typeof record.description === 'string' &&
    typeof record.status === 'number' &&
    Number.isFinite(record.status) &&
    typeof record.reportedAtUtc === 'string'
  )
}

export async function getIncidents() {
  const response = await httpClient.get<IncidentListItem[]>('/incidents')

  if (!Array.isArray(response.data) || !response.data.every(isIncidentListItem)) {
    throw new Error('Incidents response is invalid.')
  }

  return response.data
}
