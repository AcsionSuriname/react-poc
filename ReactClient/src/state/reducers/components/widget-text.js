import {
    WIDGET_TEXT_FETCH_START,
    WIDGET_TEXT_FETCH_END
} from '../../actions'

const initialState = {
    fetching: false,
    id: 0,
    title: "",
    body: "",
    image: null
};

export default function componentWidgetText(state = initialState, action) {
    switch (action.type) {
        case WIDGET_TEXT_FETCH_START:
            return {
                ...state,
                fetching: true,
                body: 'loading content'
            };
        case WIDGET_TEXT_FETCH_END:
            return {
                ...state,
                fetching: false,
                id: action.payload.id,
                title: action.payload.title,
                body: action.payload.body,
                image: action.payload.image,
            };
        default:
            return state;
    }
};
