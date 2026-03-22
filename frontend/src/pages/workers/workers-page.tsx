import { WorkersPlaceholder } from '../../features/workers/components/workers-placeholder'
import { PageHeader } from '../../shared/components/page-header'

export function WorkersPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Workers"
        title="Workforce management"
        description="Page composition is ready for worker list, detail, and create use cases."
      />
      <WorkersPlaceholder />
    </div>
  )
}
