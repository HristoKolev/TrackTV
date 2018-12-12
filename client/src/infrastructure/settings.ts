export interface SettingsState {
  // sentryDns: string;
  showsPageSize: number;
}

export const settings: SettingsState = window.__injected_settings__ || {};
