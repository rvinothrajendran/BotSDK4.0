from sys import exit

class EchoBot:
    async def on_turn(self, context):
        if context.activity.type == "message" and context.activity.text:
            strlen = len(context.activity.text)
            sendInfo = "Hey you send text : " + context.activity.text + "  and  the lenght of the string is  " + str(strlen)
            await context.send_activity(sendInfo)
