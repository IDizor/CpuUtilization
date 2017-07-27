import { CpuWebPage } from './app.po';

describe('cpu-web App', () => {
  let page: CpuWebPage;

  beforeEach(() => {
    page = new CpuWebPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
