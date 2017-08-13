import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

export const smartComponent: Component = {
    changeDetection: ChangeDetectionStrategy.Default,
    encapsulation: ViewEncapsulation.Emulated,
};

export const presentationComponent: Component = {
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation: ViewEncapsulation.Emulated,
};
