import { Component, View } from 'angular2/angular2';
import { ListComponent } from 'admin/ui';

@Component({ selector: 'users-list' })
@View({ templateUrl: 'UsersListComponent.html' })
export class UsersListComponent extends ListComponent {
    constructor() {
        super();
    }
}