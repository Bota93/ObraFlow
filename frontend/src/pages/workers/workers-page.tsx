import { WorkersTable } from '../../features/workers/components/workers-table'
import { PageHeader } from '../../shared/components/page-header'

export function WorkersPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Workers"
        title="Workforce management"
        description="Current workforce data pulled from the backend API."
      />
      <WorkersTable />
    </div>
  )
}
