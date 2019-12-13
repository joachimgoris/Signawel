import { ReportImageModel } from './report-image.model';
import { ReportIssueModel } from './report-issue.model';

export class ReportModel {
    id: string;
    roadworkId: number;
    senderEmail: string;
    creationTime: string;
    issue: ReportIssueModel
    description: string;
    images: ReportImageModel[]
}