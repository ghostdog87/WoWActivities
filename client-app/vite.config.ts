import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react-swc';
import mkcert from'vite-plugin-mkcert'

export default defineConfig({
    build: {
        outDir: '../API/wwwroot'
    },
    server: {
        port: 3001,
    },
    plugins: [react(), mkcert()]
})