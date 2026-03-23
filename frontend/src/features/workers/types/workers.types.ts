export type WorkerListItem = {
  id: string
  name: string
  role: string
  phoneNumber: string
  hourlyRate: number
  createdAtUtc: string
  isActive: boolean
}

export type CreateWorkerRequest = {
  name: string
  role: string
  phoneNumber: string
  hourlyRate: number
  isActive: boolean
}

export type WorkerResponse = WorkerListItem
