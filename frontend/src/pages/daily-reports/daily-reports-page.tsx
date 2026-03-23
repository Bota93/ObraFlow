import { DailyReportsFeed } from '../../features/daily-reports/components/daily-reports-feed'
import { PageHeader } from '../../shared/components/page-header'

export function DailyReportsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Daily Reports"
        title="Daily site reporting"
        description="Recent site reports loaded from the backend API."
      />
      <DailyReportsFeed />
    </div>
  )
}
