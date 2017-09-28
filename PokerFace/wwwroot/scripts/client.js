
document.addEventListener('DOMContentLoaded',
    () => {

        const connect = document.getElementById('connect');
        const endpoint = document.getElementById('endpoint');
        const sockets = document.getElementById('sockets');

        connect.onclick = () => {
            const definition = document.getElementById('socket-instance');
            const url = endpoint.value;
            const websocket = new WebSocket(url);

            const template = document.importNode(definition.content, true);
            const id = `websocket-${Date.now()}`;
            const messages = template.querySelector('.messages');

            template.querySelector('.socket').setAttribute('id', id);
            template.querySelector('.url span.text').innerText = endpoint.value;
            template.querySelectorAll('button').forEach(b => b.setAttribute('disabled', null));

            template.querySelectorAll('button[pf-command]')
                .forEach(b => b.onclick = (e) => {
                    const name = e.target.getAttribute('pf-command');
                    const namespace = e.target.getAttribute('pf-namespace');
                    const command = {
                        name,
                        typeName: `${namespace}.${name}`
                    };

                    console.log(command);
                    websocket.send(JSON.stringify(command));
                });

            template.querySelector('button.disconnect').onclick = () => {
                websocket.close();
                const element = document.getElementById(id);
                element.parentNode.removeChild(element);
            };

            sockets.appendChild(template);

            websocket.onopen = () => {
                const append = (text) => {
                    const message = document.createElement('div');
                    message.innerText = text;
                    messages.appendChild(message);
                    messages.insertBefore(message, sockets.querySelector('.messages')[0]);
                };

                append(`websocket.open: ${id}`);

                const section = document.getElementById(id);
                section.querySelectorAll('button').forEach(b => b.removeAttribute('disabled'));

                websocket.onclose = () => {
                    console.log(`websocket.close: ${id}`);
                };

                websocket.onmessage = (e) => {
                    const data = JSON.parse(e.data);
                    append(`websocket.message: ${id} - ${e.data}`);
                    console.log('websocket.message', e, data);
                };
            };
        };

    });