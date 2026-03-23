import { useQuery } from '@tanstack/react-query'
import { getWorkers } from '../api/get-workers'

export function useWorkers() {
  return useQuery({
    queryKey: ['workers', 'list'],
    queryFn: getWorkers,
  })
}
