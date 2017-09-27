interface IGameModel {
    gameId: string;
    name: string;
}

class App {
    private readonly promise: Promise<void>;
    private readonly socket: WebSocket;
    private readonly url: URL;

    constructor(url: URL) {
        this.socket = new WebSocket(url.toString());
        this.socket.onmessage = this.onMessage;

        this.promise = new Promise<void>((resolve, reject) => {
            this.socket.onclose = (ev: CloseEvent): any => {
                reject();
            };

            this.socket.onopen = (ev: Event): any => {
                resolve();
            }
        });
    }

    public initialized(): Promise<void> {
        return this.promise;
    }

    public createGame(): void {
        const command: any = {
            name: 'NewGame',
            typeName: 'PokerFace.'
        };

        const json = JSON.stringify(command);
        console.log(command.name, json);

        this.socket.send(json);
    }

    private onMessage= (ev: MessageEvent): any => {
        console.log('data', ev.data);
    }
}

const startup = async (url: string) => {
    const app = new App(new URL(url));
    await app.initialized();

    app.createGame();
};

startup('ws://localhost:5000/ws');

