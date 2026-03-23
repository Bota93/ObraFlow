import { httpClient } from '../../../shared/api/http-client'
import type { WorkerListItem } from '../types/workers.types'

export async function getWorkers() {
  const response = await httpClient.get<WorkerListItem[]>('/workers')
  return response.data
}
