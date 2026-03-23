import { useMutation, useQueryClient } from '@tanstack/react-query'
import { createWorker } from '../api/create-worker'
import type { CreateWorkerRequest, WorkerResponse } from '../types/workers.types'

export function useCreateWorker() {
  const queryClient = useQueryClient()

  return useMutation<WorkerResponse, Error, CreateWorkerRequest>({
    mutationFn: createWorker,
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ['workers', 'list'],
      })
    },
  })
}
