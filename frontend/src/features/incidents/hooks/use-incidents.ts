import { useQuery } from '@tanstack/react-query'
import type { IncidentListItem } from '../types/incidents.types'
import { getIncidents } from '../api/get-incidents'

export function useIncidents() {
  return useQuery<IncidentListItem[], Error>({
    queryKey: ['incidents', 'list'],
    queryFn: getIncidents,
  })
}
