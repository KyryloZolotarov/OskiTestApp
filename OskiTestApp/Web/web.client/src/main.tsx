import React from 'react';
import { createRoot } from 'react-dom/client'; // Import createRoot
import App from './App';

const rootElement = document.getElementById('root');

if (rootElement) {
    const root = createRoot(rootElement); // Create a root.
    root.render(<App />); // Use the render method on the root.
}
