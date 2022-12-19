<template>
    <footer class="bg-dark  text-white pt-5 pb-4 footer">
      <div class="container text-center">
    <div class="row text-left text-md-left">

      <div class="col-md-3">
        <div class="column" style="height: 250px">
          <div class="col">
            <img src="img/logo_white.svg" />
          </div>
          <div class="col">
            <p style="font-size: 12px; color: #F9F9F9;">Мы в соц сетях</p>
            <div class="row">
              <a :href="'/'" class="icon-space"> <img src="img/icons/telegram.svg" /></a>
              <a :href="'/'" class="icon-space"> <img src="img/icons/whatsapp.svg" /></a>
              <a :href="'https://www.instagram.com/invictus.academy.kz/'" class="icon-space">
                <img src="img/icons/instagram.svg" /></a>
              <a :href="'https://www.youtube.com/@invictusfitness669'" class="icon-space">
                <img src="img/icons/youtube.svg" /></a>
            </div>
          </div>
          <div class="col" style="font-size: 14px; font-weight: 400; color: #B3B3B3;">
            © 2022 Invictus Academy
          </div>
        </div>
      </div>

      <div class="col-md-3">
        <div class="column" style="height: 220px">
          <div class="col-2" style="font-size: 12px; font-weight: 400; color: #B3B3B3;">
            Контактная информация:
          </div>
          <div class="col" style="font-size: 16px; font-weight: 300; color: #F9F9F9">
            <p> +7 (775) 887 18 76</p>
            <p> +7 (775) 887 18 76</p>
            <p>lessons@invictusacademy.kz</p>
            <p>
              <a :href="'https://invictusacademy.kz/'" style="font-size: 16px; font-weight: 300; color: #F9F9F9">
                invictusacademy.kz </a>
            </p>
          </div>
        </div>
      </div>

      <div class="col-md-3">
        <div class="column" style="height: 220px">
          <div class="col-2" style="font-size: 12px; font-weight: 400; color: #B3B3B3;">
            Навигация:
          </div>
          <div class="col">
            <p>
              <a :href="'/'" style="font-size: 18px; font-weight: 400; color: #F9F9F9">
                Главная </a>
            </p>
            <p>
              <a :href="'/user/AllCoursesPage'" style="font-size: 18px; font-weight: 400; color: #F9F9F9">
                Курсы </a>
            </p>
            <p style="font-size: 18px; font-weight: 400; color: #F9F9F9">Чат</p>
            <p style="font-size: 18px; font-weight: 400; color: #F9F9F9">Статьи</p>
          </div>
        </div>
      </div>

      <div class="col-md-3">
        <div class="column" style="height: 220px">
          <div class="col" style="font-size: 20px; font-weight: 500; color: #F9F9F9">
            Оставьте заявку и мы обязательно перезвоним!
          </div>
          <div class="col">
            <div class="row">
              <request-button />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  </footer>
</template>

<script>
import RequestButton from 'components/RequestButton.vue'
    export default {
      components: {
        RequestButton
  },
    }
</script>

<style scoped>

.footer {
  padding: 48px 58px 20px;
  margin-left: 0px;

  position: relative;

  width: 100%;

  background: #242424;
}
</style>
