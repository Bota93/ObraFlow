import { httpClient } from '../../../shared/api/http-client'
import type { WorkerListItem } from '../types/workers.types'

function isWorkerListItem(value: unknown): value is WorkerListItem {
  if (typeof value !== 'object' || value === null) {
    return false
  }

  const record = value as Record<string, unknown>

  return (
    typeof record.id === 'string' &&
    typeof record.name === 'string' &&
    typeof record.role === 'string' &&
    typeof record.phoneNumber === 'string' &&
    typeof record.hourlyRate === 'number' &&
    Number.isFinite(record.hourlyRate) &&
    typeof record.isActive === 'boolean'
  )
}

export async function getWorkers() {
  const response = await httpClient.get<WorkerListItem[]>('/workers')

  if (!Array.isArray(response.data) || !response.data.every(isWorkerListItem)) {
    throw new Error('Workers response is invalid.')
  }

  return response.data
}
