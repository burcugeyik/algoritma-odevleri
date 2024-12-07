# ATM Uygulaması
class ATM:
    def __init__(self, kullanıcı_adı, şifre, bakiye):
        self.kullanıcı_adı = kullanıcı_adı
        self.şifre = şifre
        self.bakiye = bakiye

    def giriş(self, kullanıcı_adı, şifre):
        if self.kullanıcı_adı == kullanıcı_adı and self.şifre == şifre:
            print(f"\nGiriş başarılı! Hoş geldiniz. Başlangıç bakiyeniz: {self.bakiye:.2f} TL")
            return True
        else:
            print("\nGiriş başarısız! Kullanıcı adı veya şifre yanlış.")
            return False

    def bakiye_sorgula(self):
        print(f"\nMevcut bakiyeniz: {self.bakiye:.2f} TL")

    def para_yatır(self, miktar):
        if miktar > 0:
            self.bakiye += miktar
            print(f"\n{miktar:.2f} TL hesabınıza yatırıldı.")
            self.bakiye_sorgula()
        else:
            print("\nLütfen geçerli bir miktar girin.")

    def para_çek(self, miktar):
        if miktar > 0 and miktar <= self.bakiye:
            self.bakiye -= miktar
            print(f"\n{miktar:.2f} TL hesabınızdan çekildi.")
            self.bakiye_sorgula()
        elif miktar > self.bakiye:
            print("\nYetersiz bakiye!")
        else:
            print("lütfen geç")
