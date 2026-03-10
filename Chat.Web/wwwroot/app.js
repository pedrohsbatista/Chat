window.chat = {
    connection: null,
    startConnection: async function (hubUrl, dotNetObject) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl)
            .withAutomaticReconnect()
            .build();

        this.connection.on("ReceiveMessage", (message) => {
            dotNetObject.invokeMethodAsync("ReceiveMessage", message);
        });

        this.connection.start();
    },
    sendMessage: async function (message) {        
        await this.connection.invoke("SendMessage", message);
    }
}
