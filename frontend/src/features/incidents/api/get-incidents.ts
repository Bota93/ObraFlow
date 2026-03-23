import { httpClient } from '../../../shared/api/http-client'
import type { IncidentListItem } from '../types/incidents.types'

export async function getIncidents() {
  const response = await httpClient.get<IncidentListItem[]>('/incidents')
  return response.data
}
