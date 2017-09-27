
document.addEventListener('DOMContentLoaded', () => {

    const connect = document.getElementById('connect');
    const endpoint = document.getElementById('endpoint');
    const sockets = document.getElementById('sockets');

    connect.onclick = () => {
        const template = document.getElementById('socket-instance');
        const url = endpoint.value;
        const websocket = new WebSocket(url);

        const clone = document.importNode(template.content, true);
        const id = `websocket-{Date.now()}`;
        const messages = clone.querySelector('.messages');

        clone.querySelector('.socket').setAttribute('id', id);
        clone.querySelector('.url span.text').innerText = endpoint.value;

        clone.querySelector('button').onclick = () => {
            websocket.close();
            const element = document.getElementById(id);
            element.parentNode.removeChild(element);
        }

        sockets.appendChild(clone);

        websocket.onopen = () => {
            const write = (text) => {
                const message = document.createElement('div');
                message.innerText = text;
                messages.appendChild(message);
            }

            write(`websocket.open: {id}`);

            websocket.onclose = () => {
                console.log(`websocket.close: {id}`);
            }

            websocket.onmessage = (e) => {
                write(`websocket.message: {id}`);
                console.log('websocket.message', e);
            }

            const command = {
                data: {},
                name: 'NewGame',
                typeName: 'PokerFace.Services.Requests.NewGame'
            };
            websocket.send(JSON.stringify(command));
        }
    }
    
});
