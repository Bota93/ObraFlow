import { useQuery } from '@tanstack/react-query'
import { getIncidents } from '../api/get-incidents'

export function useIncidents() {
  return useQuery({
    queryKey: ['incidents', 'list'],
    queryFn: getIncidents,
  })
}
