import React from 'react';
import './text.css';

export default function TextView({ componentWidgetText }) {

    return (
        <div>
            <h1>{componentWidgetText.title}</h1>
            <p>{componentWidgetText.body}</p>
        </div>
    );
}