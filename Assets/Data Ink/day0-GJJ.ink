VAR recipe_correct = false
VAR recipe_wrong = false

-> Day0

=== Day0 ===
-> sec1


=== sec1 ===
#speaker: Jeremy
"fiuhhhhh, akhirnya selesai juga membuat boba"

#speaker: Jeremy
"......."

#speaker: Jeremy
"sudah siang juga"

#speaker: Jeremy
"waktunya membuka Toko The Best Bobba Shop In Java"


"beberapa jam kemudian"

#speaker: Jeremy
"Ini kembalian nya kaka terimakasih dan datang lagi ya"

#speaker: Gadis Asing
"Terimakasih Juga"

#sfx: belldor

#speaker: Jeremy
"......."

#speaker: Jeremy
"masih siang, dan lumayan lah untuk saat ini"

#speaker: Jeremy
"lebih baik daripada hari kemarin"

#sfx: belldor

"(Gadis Asing Muncul)"
#speaker: Jeremy
"selamat datang di The Best Bobba Shop in java"

#speaker: Jeremy
"Ada yang bisa Dibantu?"

#speaker: Gadis Baru
"Aku Mau memesan sesuatu yang Populer disini apa ya?"

#speaker: Jeremy
"saat ini yang populer disini adalah Green Tea Bobba Cheese"

#speaker: Jeremy
"dengan perpaduan Green tea yang khas ditambah kenyal nya Bobba Premium dan ditutup dengan Creamy Keju parut"

#speaker: Jeremy
"Dengan perpaduan kombinasi itu membuat minumanya populer di kalangan wanita wanita sekarang"

#speaker: Jeremy
"Bagaimana? Tertarik Mencoba?"

#speaker: Gadis Baru
"Hmmmm."

#speaker: Gadis Baru
#animation: exited
"Sepertinya enak boleh deh"

#speaker: Gadis Baru
#animation: exited
"satu Green tea Bobba Cheese"

#speaker: Jeremy
"Green tea Bobba Cheese satu, ditunggu ya"

 -> check_recipe

=== check_recipe ===
{ 
    - recipe_correct: 
        -> correct_recipe_path
    - recipe_wrong:
        -> wrong_recipe_path
    - else:
        -> check_recipe
}

=== wrong_recipe_path ===
#speaker: Gadis Baru
#animation: disappointed
"eehhhh, apakah ini benar, kelihatannya tidak seperti Green Tea Bobba Cheese"

#speaker: Gadis Baru
#animation: disappointed
"bisa tolong maaf banget, cek lagi"


#speaker: Jeremy
"Aduh maaf, saya sedang tidak fokus"

#speaker: Jeremy
"saya buatkan lagi ya."

-> check_recipe
=== correct_recipe_path ===

#speaker: Jeremy
"ini dia Green tea Bobba Cheese datang"

#speaker: Jeremy
"silahkan dinikmati"

#speaker: Gadis Baru
#animation: exited
"woahhh, jadi ini ya yang popuuler di sosmed saat ini"

#speaker: Gadis Baru
"sebelum di minum, seperti biasa foto dulu"

#speaker: Gadis Baru
"kak bisa bantu fotoin?"

#speaker: Jeremy
"boleh, siap ya"

#speaker: Jeremy
"1...2...3..."

#sfx: capturepicture

#speaker: Gadis Baru
"hehe lucu, terimakasih kak fotonya bagus"

#speaker: Jeremy
"ahh biasa saja"

#speaker: Jeremy
"tapi terimakasih"

#speaker: Gadis Baru
"aku cobain ya kak"

#speaker: Jeremy
"silakan, kalo tidak enak bilang saja"

#speaker: Jeremy
"garansi uang kembali jika tidak enak"

#speaker: Gadis Baru
"kaka bisa saja"

#sfx: sruput

#speaker: Gadis Baru
"..........."

#speaker: Gadis Baru
"kak ini....."

#speaker: Gadis Baru
#animation: exited
"enakkkk, bangggettttt"

#speaker: Gadis Baru
#animation: exited
"Grenn tea nya khas banggeeet rasanya manis manis gimana gituh"

#speaker: Gadis Baru
#animation: exited
"ditambah ini kenyal manis bobba nya tercampur rata"

#speaker: Gadis Baru
#animation: exited
"diakhiri dengan creamy keju, ngebuat ini jadi Perfect bngeet"

#speaker: Gadis Baru
#animation: exited
"(yapping yapping)"

#speaker: Jeremy
"ah iya kak terimaksih atas ulasannya"

#speaker: Gadis Baru
#animation: exited
"(lanjut yapping)"

#speaker: Jeremy
"ah btw kak, saya baru pertama kali liat kaka di sini"

#speaker: Stephanie
"ah maaf kak sebelumnya, perkenalkan nama aku Stephanie"

#speaker: Stephanie
"aku baru pindah ke sekitaran sini seminggu yang lalu"

#speaker: Stephanie
"aku pindah karena ngikut papa kerja di kantor sekitaran sini"

#speaker: Stephanie
"jadi aku keliling-keliling buat ngehafalin area sekitaran sini"

#speaker: Jeremy
"ah saya Jeremy, salam kenal"

#speaker: Jeremy
"saya penjaga(Pemilik) toko Bobba ini"

#speaker: Jeremy
"ya baru beberapa bulan lah"

#speaker: Stephanie
#animation: exited
"woahh jadi kaka......"
#sfx: messagein

#speaker: Jeremy
"ada pesan masuk tuh"

#speaker: Stephanie
"ah iya dari papah ternyata"

#speaker: Stephanie
"aku disuruh pulang"

#speaker: Stephanie
"yasudah aku bayar pake QRIS ya kak"

#speaker: Stephanie
"total nya segini kan?"

#speaker: Jeremy
"iya betul"

#sfx: notofication_in

#speaker: Jeremy
"siap masuk"

#speaker: Stephanie
"kalo begitu terimakasih kak, aku pulang dulu"

#speaker: Jeremy
"iya terimakasih juga, hati hati dijalan"

#speaker: Jeremy
"dan datang lagi ya"

#speaker: Stephanie
"iya pasti dong"

#speaker: Stephanie
"soalnya kaka nya........"

#speaker: Stephanie
"hehe"

#sfx: belldor

#speaker: Jeremy
"......"

"(hari pun berlalu seperti biasanya)"
-> END
