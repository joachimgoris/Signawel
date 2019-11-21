export class ModalCloseEvent {
  public reason: string;

  private _isDefaultPrevented: boolean;

  constructor(reason: string) {
    this.reason = reason;
  }
  public isDefaultPrevented(): boolean {
    return this._isDefaultPrevented;
  }

  public preventDefault(): void {
    this._isDefaultPrevented = true;
  }
}
