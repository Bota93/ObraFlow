import { IncidentsFeed } from '../../features/incidents/components/incidents-feed'
import { PageHeader } from '../../shared/components/page-header'

export function IncidentsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Incidents"
        title="Incident tracking"
        description="Recent site incidents with status and reporting history."
      />
      <IncidentsFeed />
    </div>
  )
}
