import { EmailResponseModel } from './email-response-model';
import { CityResponseModel } from './city-response-model';
import { UserResponseModel } from './user-response-model';

export class ReportGroupResponseModel {
    public id: string;
    public cityReportGroups: Array<CityResponseModel> = new Array<CityResponseModel>();
    public emailReportGroups: Array<EmailResponseModel> = new Array<EmailResponseModel>();
    public userReportGroups: Array<UserResponseModel> = new Array<UserResponseModel>();
}
