import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkshopComponent } from './workshop.component';

describe('WorkshopComponent', () => {
    let fixture: ComponentFixture<WorkshopComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [WorkshopComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(WorkshopComponent);
        fixture.detectChanges();
    });

    it('should start with count 0, then increments by 1 when clicked', async(() => {
        const countElement = fixture.nativeElement.querySelector('strong');
        expect(countElement.textContent).toEqual('0');

        const incrementButton = fixture.nativeElement.querySelector('button');
        incrementButton.click();
        fixture.detectChanges();
        expect(countElement.textContent).toEqual('1');
    }));
});
