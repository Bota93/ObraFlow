import { DashboardMetrics } from '../../features/dashboard/components/dashboard-metrics'
import { PageHeader } from '../../shared/components/page-header'

export function DashboardPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Dashboard"
        title="Site activity overview"
        description="Operational summary for workforce activity, reporting, and incident status."
      />
      <DashboardMetrics />
    </div>
  )
}
