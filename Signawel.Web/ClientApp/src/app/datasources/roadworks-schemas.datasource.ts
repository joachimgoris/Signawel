import { DataSource } from "@angular/cdk/table";
import { RoadworkSchemaModel } from "../models/RoadworkSchema.model";
import { Observable, BehaviorSubject, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { RoadworkSchemasService } from "../services/roadwork-schemas/roadwork-schemas.service";
import { CollectionViewer } from "@angular/cdk/collections";

export class RoadworkSchemasDataSource extends DataSource<RoadworkSchemaModel> {
  private subject: BehaviorSubject<RoadworkSchemaModel[]> = new BehaviorSubject<
    RoadworkSchemaModel[]
  >([]);

  private loadingSubject: BehaviorSubject<boolean> = new BehaviorSubject<
    boolean
  >(false);

  public loading$ = this.loadingSubject.asObservable();

  public total: number;

  constructor(private service: RoadworkSchemasService) {
    super();
  }

  data(): RoadworkSchemaModel[] {
    return this.subject.value;
  }

  connect(
    collectionViewer: CollectionViewer
  ): Observable<RoadworkSchemaModel[]> {
    return this.subject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.subject.complete();
    this.loadingSubject.complete();
  }

  loadWorkroadSchemas(
    search: string = "",
    sortDirection: string = "asc",
    pageIndex: number = 0,
    pageSize: number = 15
  ) {
    this.loadingSubject.next(true);

    this.service
      .searchRoadworkSchemas(search, sortDirection, pageIndex, pageSize)
      .pipe(
        catchError(() => of(null)),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe(result => {
        this.total = result.total;
        this.subject.next(result.schemas);
      });
  }
}
