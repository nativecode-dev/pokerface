var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class App {
    constructor(url) {
        this.onMessage = (ev) => {
            console.log('data', ev.data);
        };
        this.socket = new WebSocket(url.toString());
        this.socket.onmessage = this.onMessage;
        this.promise = new Promise((resolve, reject) => {
            this.socket.onclose = (ev) => {
                reject();
            };
            this.socket.onopen = (ev) => {
                resolve();
            };
        });
    }
    initialized() {
        return this.promise;
    }
    createGame() {
        const command = {
            name: 'NewGame',
            typeName: 'PokerFace.'
        };
        const json = JSON.stringify(command);
        console.log(command.name, json);
        this.socket.send(json);
    }
}
const startup = (url) => __awaiter(this, void 0, void 0, function* () {
    const app = new App(new URL(url));
    yield app.initialized();
    app.createGame();
});
startup('ws://localhost:5000/ws');
//# sourceMappingURL=App.js.map