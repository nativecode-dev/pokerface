

/**
 * Home
 */
export class Home {
    private static readonly resourceName: string = 'home';

    private readonly baseUrl: URL;

    constructor(baseUrl: URL) {
        this.baseUrl = new URL(Home.resourceName, baseUrl.toString());
    }
    
    /**
     *  Index
     *  @return {string}
     */
    public index(): Promise<string> {
        // HttpGet=
        const url = this.baseUrl;
        console.log('index', url);
        return Promise.resolve(null);
    }

}