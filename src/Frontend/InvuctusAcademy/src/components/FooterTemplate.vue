<template>
    <footer class="footer">
    <q-toolbar class="row">

      <div class="col-3">
        <div class="column" style="height: 250px">
          <div class="col">
            <img src="img/logo_white.svg" />
          </div>
          <div class="col">
            <p style="font-size: 12px; color: #F9F9F9;">Мы в соц сетях</p>
            <div class="row">
              <div class="icon-space"> <img src="img/icons/telegram.svg" /></div>
              <div class="icon-space"> <img src="img/icons/whatsapp.svg" /></div>
              <div class="icon-space"> <img src="img/icons/instagram.svg" /> </div>
              <div class="icon-space"> <img src="img/icons/youtube.svg" /></div>
            </div>
          </div>
          <div class="col" style="font-size: 14px; font-weight: 400; color: #B3B3B3;">
            © 2022 Invictus Academy
          </div>
        </div>
      </div>

      <div class="col-3">
        <div class="column" style="height: 220px">
          <div class="col-2" style="font-size: 12px; font-weight: 400; color: #B3B3B3;">
            Контактная информация:
          </div>
          <div class="col" style="font-size: 16px; font-weight: 300; color: #F9F9F9">
            <p> +7 (775) 887 18 76</p>
            <p> +7 (775) 887 18 76</p>
            <p>arystan-bakhtiyarov@mail.ru</p>
            <p>www.invictus.kz</p>
          </div>
        </div>
      </div>

      <div class="col-2">
        <div class="column" style="height: 220px">
          <div class="col-2" style="font-size: 12px; font-weight: 400; color: #B3B3B3;">
            Навигация:
          </div>
          <div class="col" style="font-size: 18px; font-weight: 400; color: #F9F9F9">
            <p>Главная</p>
            <p>Курсы</p>
            <p>Чат</p>
            <p>Статьи</p>
          </div>
        </div>
      </div>

      <div class="col-4">
        <div class="column" style="height: 220px">
          <div class="col" style="font-size: 20px; font-weight: 500; color: #F9F9F9">
            Подпишитесь на нашу рассылку, чтобы первым быть в курсе акций и скидок!
            <p style="font-size: 14px; font-weight: 300; color: #B3B3B3;">
              Никакого спама. Только самые ценные советы</p>
          </div>
          <div class="col">
            <div class="row">
              <q-input style="background-color: white; width: 250px; margin-right: 5px;" filled outlined
                label="example@mail.com" />
              <q-btn color="accent" label="Подписаться" />
            </div>
          </div>
        </div>
      </div>
    </q-toolbar>
  </footer>
</template>

<script>
    export default {
        
    }
</script>

<style scoped>
    
.footer {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-start;
  padding: 48px 58px 20px;
  margin-left: 0px;

  position: relative;

  width: 100%;

  background: #242424;
}
</style>