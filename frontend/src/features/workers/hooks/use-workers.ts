import { useQuery } from '@tanstack/react-query'
import type { WorkerListItem } from '../types/workers.types'
import { getWorkers } from '../api/get-workers'

export function useWorkers() {
  return useQuery<WorkerListItem[], Error>({
    queryKey: ['workers', 'list'],
    queryFn: getWorkers,
  })
}
