import {
  MESSAGES_FETCH_START,
  MESSAGES_FETCH_END
} from '../../actions'

const initialState = {
  count: 0,
  unreadCount: 0,
  messages: [],
  fetching: false
};

export default function componentWidgetMessages(state = initialState, action) {
  switch (action.type) {
    case MESSAGES_FETCH_START:
      return {
        ...state,
        fetching: true
      };
    case MESSAGES_FETCH_END:

      const receivedMessages = [];
      if (action.payload && action.payload.messages && action.payload.messages.length > 0) {
        for (let receivedMessage of action.payload.messages) {
          receivedMessages.push({
            id: receivedMessage.messageGID,
            subjectText: receivedMessage.subjectText,
            bodyText: receivedMessage.bodyText,
            patientGID: receivedMessage.patientGID,
            messageTypeId: receivedMessage.messageTypeId,
            dateCreated: receivedMessage.dateCreated
          });
        }
      }

      return {
        ...state,
        count: action.payload.count,
        unreadCount: action.payload.unreadCount,
        messages: receivedMessages,
        fetching: false
      };

    default:
      return state;
  }
};
