import {
  Component,
  OnInit,
  AfterViewInit,
  ViewChild,
  ElementRef
} from "@angular/core";
import { SelectionModel } from "@angular/cdk/collections";
import { MatPaginator, MatSort } from "@angular/material";
import { tap, debounceTime, distinctUntilChanged } from "rxjs/operators";
import { merge, fromEvent } from "rxjs";

import { RoadworkSchemasDataSource } from "../../datasources/roadworks-schemas.datasource";
import { RoadworkSchemasService } from "../../services/roadwork-schemas/roadwork-schemas.service";
import { RoadworkSchemaModel } from "../../models/roadwork-schema.model";
import { BladeModalService } from "../../../shared/services/blade-modal.service";
import { ModalCloseEvent } from "../../../shared/components/blade-modal/modal-close-event";

@Component({
  selector: "determination-schema-list",
  templateUrl: "./schema-list.component.html",
  styleUrls: ["./schema-list.component.sass"]
})
export class SchemaListComponent implements OnInit, AfterViewInit {
  listData: RoadworkSchemasDataSource;
  selectedRoadworkSchema: RoadworkSchemaModel;

  displayedColumns: string[] = ["select", "name", "actions"];
  selection = new SelectionModel<RoadworkSchemaModel>(true, []);

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild("searchInput", { static: false }) searchInput: ElementRef;

  constructor(
    private service: RoadworkSchemasService,
    private modalService: BladeModalService
  ) {}

  ngOnInit() {
    this.listData = new RoadworkSchemasDataSource(this.service);
    this.listData.loadWorkroadSchemas();
  }

  ngAfterViewInit(): void {
    fromEvent(this.searchInput.nativeElement, "keyup")
      .pipe(
        debounceTime(150),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.loadWorkroadSchemas();
        })
      )
      .subscribe();

    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(tap(() => this.loadWorkroadSchemas()))
      .subscribe();
  }

  loadWorkroadSchemas() {
    this.listData.loadWorkroadSchemas(
      this.searchInput.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.listData.data().length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.listData.data().forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: RoadworkSchemaModel): string {
    if (!row) {
      return `${this.isAllSelected() ? "select" : "deselect"} all`;
    }

    return `${
      this.selection.isSelected(row) ? "deselect" : "select"
    } row ${row.position + 1}`;
  }

  onNewClicked() {
    console.log(this.selection.selected);
  }

  doEdit(schema: RoadworkSchemaModel) {
    console.log(schema);
    this.selectedRoadworkSchema = JSON.parse(JSON.stringify(schema));
    this.modalService.open("schemaEditor");
  }

  onModalClose(event: ModalCloseEvent) {
    if (event.reason == "background") {
      event.preventDefault();
      return;
    }

    this.selectedRoadworkSchema = null;
  }

  onSchemaEditorCancel() {
    this.selectedRoadworkSchema = null;
    this.modalService.close("schemaEditor");
  }

  onSchemaEditorSave() {
    if (!this.selectedRoadworkSchema.id) {
      this.service
        .createRoadworkSchema(this.selectedRoadworkSchema)
        .subscribe(res => {
          this.selectedRoadworkSchema = null;
          this.modalService.close("schemaEditor");
          this.loadWorkroadSchemas();
        });
    } else {
      this.service
        .updateRoadworkSchema(this.selectedRoadworkSchema)
        .subscribe(res => {
          this.selectedRoadworkSchema = null;
          this.modalService.close("schemaEditor");
          this.loadWorkroadSchemas();
        });
    }
  }

  createNew() {
    this.selectedRoadworkSchema = new RoadworkSchemaModel();
    this.modalService.open("schemaEditor");
  }

  delete(id: string) {
    this.service.deleteRoadworkSchema(id).subscribe(res => {
      this.loadWorkroadSchemas();
    });
  }
}
