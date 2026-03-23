import { httpClient } from '../../../shared/api/http-client'
import type { CreateWorkerRequest, WorkerResponse } from '../types/workers.types'

function isWorkerResponse(value: unknown): value is WorkerResponse {
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

export async function createWorker(payload: CreateWorkerRequest) {
  const response = await httpClient.post<WorkerResponse>('/workers', payload)

  if (!isWorkerResponse(response.data)) {
    throw new Error('Create worker response is invalid.')
  }

  return response.data
}
