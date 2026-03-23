import { IncidentsFeed } from '../../features/incidents/components/incidents-feed'
import { PageHeader } from '../../shared/components/page-header'

export function IncidentsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Incidents"
        title="Incident tracking"
        description="Recent incident activity loaded from the backend API."
      />
      <IncidentsFeed />
    </div>
  )
}
