# Етап 1: Збірка
FROM node:20-alpine AS build
WORKDIR /app

# Тепер ці файли точно будуть знайдені в контексті
COPY EventTicketBookingSystem/package.json ./
RUN npm install

COPY EventTicketBookingSystem ./
RUN npm run build

# Етап 2: Раздача статики через Nginx
FROM nginx:stable-alpine
# Vite за замовчуванням збирає проект у папку dist
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"] fix dependencies