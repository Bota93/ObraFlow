import { CreateWorkerForm } from '../../features/workers/components/create-worker-form'
import { PageHeader } from '../../shared/components/page-header'

export function CreateWorkerPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Workers"
        title="Create worker"
        description="Add a new worker to the project workforce register."
      />
      <CreateWorkerForm />
    </div>
  )
}
