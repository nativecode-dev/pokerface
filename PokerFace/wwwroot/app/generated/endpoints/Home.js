/**
 * Home
 */
export class Home {
    constructor(baseUrl) {
        this.baseUrl = new URL(Home.resourceName, baseUrl.toString());
    }
    /**
     * Index
     
     */
    index() {
        // HttpGet=
        const url = this.baseUrl;
        console.log('index', url);
        return Promise.resolve(null);
    }
}
Home.resourceName = 'home';
//# sourceMappingURL=Home.js.map