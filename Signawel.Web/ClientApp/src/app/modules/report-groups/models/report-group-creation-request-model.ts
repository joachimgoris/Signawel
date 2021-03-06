import { CityCreationRequestModel } from "./city-creation-request-model";
import { EmailCreationRequestModel } from "./email-creation-request-model";
import { UserCreationRequestModel } from './user-creation-request-model';


export class ReportGroupCreationRequestModel {
    public cityReportGroups: Array<CityCreationRequestModel> = new Array<CityCreationRequestModel>();
    public emailReportGroups: Array<EmailCreationRequestModel> = new Array<EmailCreationRequestModel>();
    public userReportGroups: Array<UserCreationRequestModel> = new Array<UserCreationRequestModel>();
}
