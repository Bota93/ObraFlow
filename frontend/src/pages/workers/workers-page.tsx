import { Link } from 'react-router-dom'
import { WorkersTable } from '../../features/workers/components/workers-table'
import { PageHeader } from '../../shared/components/page-header'

export function WorkersPage() {
  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
        <PageHeader
          eyebrow="Workers"
          title="Workforce management"
          description="Current workforce across the project, including roles, contact data, and status."
        />
        <Link
          to="/workers/new"
          style={{ color: '#ffffff' }}
          className="inline-flex items-center justify-center rounded-full bg-slate-950 px-5 py-3 text-sm font-semibold text-white transition hover:bg-slate-800 hover:text-white visited:text-white"
        >
          Create worker
        </Link>
      </div>
      <WorkersTable />
    </div>
  )
}
